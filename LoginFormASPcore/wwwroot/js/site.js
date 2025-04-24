// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onscroll = function () {
    document.getElementById('scrollTopBtn').style.display =
        window.scrollY > 200 ? 'block' : 'none';
};
document.getElementById('scrollTopBtn').onclick = function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};



// JavaScript for Scroll-Based Animation
document.addEventListener("scroll", () => {
    const elements = document.querySelectorAll(".animate-slide-up, .animate-fade-in");
    elements.forEach((el) => {
        const rect = el.getBoundingClientRect();
        if (rect.top < window.innerHeight - 50) {
            el.classList.add("visible");
        }
    });
});

// Add 'visible' class for animations
document.querySelectorAll(".animate-slide-up, .animate-fade-in").forEach((el) => {
    el.classList.add("visible");
});