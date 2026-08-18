import type { InputHTMLAttributes, TextareaHTMLAttributes } from "react";

type SharedProps = { label: string; hint?: string };
type InputProps = SharedProps & InputHTMLAttributes<HTMLInputElement> & { multiline?: false };
type TextAreaProps = SharedProps & TextareaHTMLAttributes<HTMLTextAreaElement> & { multiline: true };

const controlClass = "mt-2 w-full rounded-lg border border-ink/15 bg-white px-3.5 py-3 text-sm text-ink outline-none transition placeholder:text-muted/60 focus:border-moss focus:ring-3 focus:ring-moss/10";

const FormField = (props: InputProps | TextAreaProps) => {
  const { label, hint, multiline, ...controlProps } = props;
  const id = controlProps.id;
  return (
    <div>
      <label htmlFor={id} className="text-sm font-bold text-ink">{label}</label>
      {multiline ? <textarea {...controlProps as TextareaHTMLAttributes<HTMLTextAreaElement>} className={`${controlClass} min-h-28 resize-y`} /> : <input {...controlProps as InputHTMLAttributes<HTMLInputElement>} className={controlClass} />}{hint && <p className="mt-1.5 text-xs text-muted">{hint}</p>}
    </div>
  );
};
export default FormField;
