'use strict';

function footerPosition(element, scrollHeight, innerHeight) {
    try {
        var _element = document.querySelector(element);
        var isLargeScreenHeight = scrollHeight >= innerHeight + _element.scrollHeight;

        _element.classList.toggle('position-fixed', !isLargeScreenHeight);
        _element.classList.toggle('position-static', isLargeScreenHeight);
    } catch (error) {
        console.error(error);
    }
}

footerPosition('footer', document.body.scrollHeight, window.innerHeight);

function toggleMenu(attribute) {
    try {
        (function () {
            var toggleBtn = document.querySelector(attribute);
            toggleBtn.addEventListener('click', function () {
                var element = document.querySelector(toggleBtn.getAttribute('data-target'));

                if (!element.classList.contains('open-menu')) {
                    element.classList.add('open-menu');
                    toggleBtn.classList.add('btn-outline-dark');
                    toggleBtn.classList.add('btn-toggle-white');
                } else {
                    element.classList.remove('open-menu');
                    toggleBtn.classList.remove('btn-outline-dark');
                    toggleBtn.classList.remove('btn-toggle-white');
                }
            });
        })();
    } catch (error) {
        console.error(error);
    }
}

toggleMenu('[data-option="toggle"]');

