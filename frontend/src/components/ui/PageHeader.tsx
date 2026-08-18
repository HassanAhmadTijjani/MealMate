import type { ReactNode } from "react";

type PageHeaderProps = { eyebrow?: string; title: string; description?: string; action?: ReactNode };

const PageHeader = ({ eyebrow, title, description, action }: PageHeaderProps) => (
  <div className="mb-10 flex flex-col gap-5 border-b border-ink/10 pb-8 sm:flex-row sm:items-end sm:justify-between">
    <div>{eyebrow && <p className="mb-2 text-xs font-bold uppercase tracking-[.18em] text-coral">{eyebrow}</p>}
      <h1 className="display text-3xl font-extrabold sm:text-4xl">{title}</h1>
      {description && <p className="mt-3 max-w-2xl text-muted">{description}</p>}
    </div>
    {action && <div className="shrink-0">{action}</div>}
  </div>
);
export default PageHeader;
