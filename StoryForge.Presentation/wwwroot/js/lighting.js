const themeKey = 'storyforge-theme';
let savedTheme;
try { savedTheme = localStorage.getItem(themeKey); } catch { /* Storage may be unavailable. */ }
document.documentElement.classList.toggle('dark',
    savedTheme === 'dark' || (savedTheme !== 'light' && window.matchMedia('(prefers-color-scheme: dark)').matches));

export function toggleDarkMode() {
    const isDark = document.documentElement.classList.toggle('dark');
    try { localStorage.setItem(themeKey, isDark ? 'dark' : 'light'); } catch { /* Keep the session preference. */ }
}

export function isDarkModeEnabled() {
    return document.documentElement.classList.contains('dark');
}
