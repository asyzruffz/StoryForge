export function toggleDarkMode() {
    document.documentElement.classList.toggle('dark');
}

export function isDarkModeEnabled() {
    return document.documentElement.classList.contains('dark');
}