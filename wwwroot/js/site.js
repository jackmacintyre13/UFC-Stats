// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function() {
  'use strict';

  function ready(fn) {
    if (document.readyState !== 'loading') fn();
    else document.addEventListener('DOMContentLoaded', fn);
  }

  ready(function() {
    const navbar = document.querySelector('.navbar-custom');
    const themeToggle = document.getElementById('themeToggle');
    const filtersToggle = document.getElementById('filtersToggle');
    const filtersPanel = document.querySelector('.controls .filters');

    // Sticky navbar background on scroll
    function updateNavbar() {
      if (!navbar) return;
      if (window.scrollY > 12) navbar.classList.add('scrolled');
      else navbar.classList.remove('scrolled');
    }
    updateNavbar();
    window.addEventListener('scroll', updateNavbar, { passive: true });

    // Theme toggle (light/dark) persisted in localStorage
    (function() {
      const key = 'ufc-theme';
      const stored = localStorage.getItem(key);
      const prefersLight = window.matchMedia && window.matchMedia('(prefers-color-scheme: light)').matches;
      if (stored === 'light') document.body.classList.add('theme-light');
      else if (!stored && prefersLight) document.body.classList.add('theme-light');

      if (themeToggle) {
        themeToggle.addEventListener('click', function() {
          const isLight = document.body.classList.toggle('theme-light');
          localStorage.setItem(key, isLight ? 'light' : 'dark');
          // update ARIA
          themeToggle.setAttribute('aria-pressed', isLight ? 'true' : 'false');
        });
      }
    })();

    // Mobile filters toggle
    if (filtersToggle && filtersPanel) {
      filtersToggle.addEventListener('click', function() {
        filtersPanel.classList.toggle('open');
        filtersPanel.style.display = filtersPanel.classList.contains('open') ? 'flex' : '';
        filtersToggle.setAttribute('aria-expanded', filtersPanel.classList.contains('open'));
      });
    }

    // Small helper: add focus visible polyfill class to keyboard users
    function handleFirstTab(e) {
      if (e.key === 'Tab') document.documentElement.classList.add('user-is-tabbing');
      window.removeEventListener('keydown', handleFirstTab);
    }
    window.addEventListener('keydown', handleFirstTab);
  });
})();
