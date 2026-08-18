# MealMate UI Guide

This guide documents the frontend design system in `frontend/src`. The backend was not changed.

## Visual direction

MealMate uses a warm, professional restaurant palette. Deep green communicates trust and hospitality, coral marks primary actions, cream softens page backgrounds, and charcoal keeps text highly readable. Typography uses Manrope for headings and DM Sans for interface text.

Global tokens are defined in `src/index.css` using Tailwind v4's `@theme`. Reuse token classes such as `bg-moss`, `text-coral`, `bg-cream`, `text-muted`, and `font-display` rather than adding slightly different colours page by page.

## Reusable components

### PageHeader

Location: `src/components/ui/PageHeader.tsx`

Use at the top of standard content pages. It supports an eyebrow, title, description, and optional action.

```tsx
<PageHeader
  eyebrow="Directory"
  title="Restaurants"
  description="Browse local places."
  action={<Link to="/restaurants/new">Add restaurant</Link>}
/>
```

### FeedbackState

Location: `src/components/ui/FeedbackState.tsx`

Use for full-page or full-section loading, empty, unavailable, and error states. Set `loading` for loading and `tone="danger"` only when something failed and the user needs to notice or recover.

```tsx
<FeedbackState loading title="Loading..." />
<FeedbackState title="Nothing here yet" description="Add the first item." />
<FeedbackState tone="danger" title="Could not load" action={<button>Try again</button>} />
```

### FormField

Location: `src/components/ui/FormField.tsx`

Use for text, email, telephone, URL, and textarea inputs. Pass `multiline` for a textarea. It provides the shared label, hint, border, focus ring, and spacing.

```tsx
<FormField id="email" label="Email" type="email" value={email} onChange={...} required />
<FormField id="bio" label="Description" value={bio} onChange={...} multiline />
```

### RestaurantCard

Location: `src/components/RestaurantCard.tsx`

Use only for a restaurant inside a responsive card grid. It handles missing images/descriptions, open/closed status, address truncation, and the details link.

## Semantic colours and when to use them

| Situation | Treatment | Example |
| --- | --- | --- |
| Primary action | Coral background, white text | Create, submit, main hero action |
| Secondary action | Moss background or neutral outline | Add from a directory, supporting action |
| Success/open | Emerald dot or a subtle green surface | Restaurant is open, save completed |
| Danger/error | Red border, pale red background, dark red text | API failed, destructive confirmation, invalid submission |
| Warning | Amber border/background/text | A choice may have consequences but is not destructive |
| Neutral/empty | White surface, ink border, muted text | No results, not found, informational state |
| Disabled | Lower opacity plus disabled cursor | Submission in progress or unavailable action |

Danger styling must be reserved for failures and destructive actions such as delete, remove, or permanently discard. Do not use red for ordinary cancel buttons. A destructive confirmation should explain what will happen and use a red final-action button; the safe escape action stays neutral.

## Layout patterns

- Page content uses `max-w-6xl px-5 sm:px-8` for consistent alignment.
- Forms use `max-w-3xl`; reading content should remain narrower than directory grids.
- Restaurant lists use `grid gap-5 sm:grid-cols-2 lg:grid-cols-3`.
- Keep page sections unframed. Cards are for individual restaurant items, feedback panels, and the form tool itself.
- Use an 8px (`rounded-lg`) radius for controls and a 16px (`rounded-2xl`) radius for true cards.

## Accessibility and interaction

- Every input needs a visible label and unique `id`.
- Errors use `role="alert"`; loading and empty states use status semantics.
- Links navigate; buttons perform actions. Do not use a clickable `div`.
- Keep keyboard focus styles. The shared `FormField` includes a visible green focus ring.
- Include disabled and loading states for async actions to prevent duplicate submissions.
- Provide useful image alt text when the image conveys content. Decorative images use an empty alt value.

## Adding a new page

1. Wrap content in a `section` with the standard max-width and responsive padding.
2. Add `PageHeader` for the page title and primary action.
3. Use `FeedbackState` for loading, empty, and failed requests.
4. Use the semantic colour table when choosing action/state colours.
5. Extract a component into `src/components/ui` when it is generic and likely to appear on multiple pages; keep domain-specific components such as `RestaurantCard` in `src/components`.
