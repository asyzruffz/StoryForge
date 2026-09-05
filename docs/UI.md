# UI

The production UI lives in `StoryForge.Presentation` and uses BlazorBlueprint. `StoryForge.Desktop` hosts it with Photino.Blazor. Use the BlazorBlueprint MCP documentation when it is available; prefer existing component patterns and Tailwind classes in nearby Razor files.

- Put routed pages in `StoryForge.Presentation/Pages`, reusable UI in `Components`, and layout elements in `Layouts`.
- Keep Razor components thin: fetch or update data through `ISender`, handle `Result` explicitly, and keep business/persistence logic in Application handlers.
- Register new UI services through the presentation or desktop composition extensions as appropriate.
- Update the desktop router when a route or fallback requires host-level wiring.
- Use `BreadCrumbHandler` to set breadcrumbs for routed pages.

`StoryForge.MudPresentation` is retained only as a migration reference. Translate its behavior to BlazorBlueprint components; do not add MudBlazor dependencies or treat it as the active UI.
