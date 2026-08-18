import type { ReactNode } from "react";

type FeedbackStateProps = { title: string; description?: string; tone?: "neutral" | "danger"; action?: ReactNode; loading?: boolean };

const FeedbackState = ({ title, description, tone = "neutral", action, loading }: FeedbackStateProps) => (
  <div className={`mx-auto max-w-lg rounded-xl border p-8 text-center ${tone === "danger" ? "border-red-200 bg-red-50" : "border-ink/10 bg-white"}`} role={tone === "danger" ? "alert" : "status"}>
    {loading && <span className="mx-auto mb-5 block size-8 animate-spin rounded-full border-2 border-moss/20 border-t-moss" />}
    {!loading && <span className={`mx-auto mb-5 grid size-10 place-items-center rounded-full text-lg font-bold ${tone === "danger" ? "bg-red-100 text-red-700" : "bg-sage text-moss"}`}>{tone === "danger" ? "!" : "M"}</span>}
    <h2 className="display text-lg font-bold">{title}</h2>{description && <p className="mt-2 text-sm text-muted">{description}</p>}{action && <div className="mt-5">{action}</div>}
  </div>
);
export default FeedbackState;
