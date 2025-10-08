const form = document.getElementById('fundingForm');
const progressFill = document.getElementById('progressFill');
const successMessage = document.getElementById('successMessage');

// Update progress bar based on filled fields
function updateProgress() {
    const inputs = form.querySelectorAll('input[required], textarea[required], select[required]');
    let filled = 0;
    inputs.forEach(input => {
        if (input.value.trim() !== '') filled++;
    });
    const progress = (filled / inputs.length) * 100;
    progressFill.style.width = progress + '%';
}

// Character counter
function updateCharCount(textarea, counterId) {
    document.getElementById(counterId).textContent = textarea.value.length;
}

// Add team member
function addTeamMember() {
    const teamMembers = document.getElementById('teamMembers');
    const newMember = document.createElement('div');
    newMember.className = 'team-member';
    newMember.innerHTML = `
                <div class="form-group">
                    <label>Full Name <span class="required">*</span></label>
                    <input type="text" name="memberName[]" required placeholder="John Doe">
                </div>
                <div class="grid-2">
                    <div class="form-group">
                        <label>Role <span class="required">*</span></label>
                        <input type="text" name="memberRole[]" required placeholder="CTO & Co-founder">
                    </div>
                    <div class="form-group">
                        <label>LinkedIn Profile</label>
                        <input type="url" name="memberLinkedin[]" placeholder="https://linkedin.com/in/...">
                    </div>
                </div>
                <div class="form-group">
                    <label>Background & Experience <span class="required">*</span></label>
                    <textarea name="memberBio[]" required placeholder="Previous experience, expertise, and achievements" rows="3"></textarea>
                </div>
            `;
    teamMembers.appendChild(newMember);
    newMember.style.animation = 'fadeInUp 0.5s ease-out';
    updateProgress();
}

// Listen for input changes
form.addEventListener('input', updateProgress);
form.addEventListener('change', updateProgress);

// Form submission
form.addEventListener('submit', function (e) {
    e.preventDefault();
    successMessage.style.display = 'block';
    window.scrollTo({ top: 0, behavior: 'smooth' });
    setTimeout(() => {
        form.reset();
        progressFill.style.width = '0%';
        document.querySelectorAll('.char-count span').forEach(span => span.textContent = '0');
    }, 2000);
});

// Initial progress update
updateProgress();



