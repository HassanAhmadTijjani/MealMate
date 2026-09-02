import { useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";
import InputField from "../components/ui/InputField";
import PageHeader from "../components/ui/PageHeader";
import { register } from "../services/AuthService";

const RegisterPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setError(null);

        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            return;
        }

        setIsSubmitting(true);

        try {
            await register({ email, password });
            navigate("/auth/login", {
                replace: true,
                state: { registrationMessage: "Account created successfully. Sign in to continue." },
            });
        } catch (error) {
            setError(error instanceof Error ? error.message : "Registration failed 🖕🏼. Damn try again.");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <section className="mx-auto max-w-3xl px-5 py-14 sm:px-8 sm:py-20">
            <PageHeader eyebrow="Join MealMate" title="Create your account" />

            <form onSubmit={handleSubmit} className="space-y-8">
                {error && (
                    <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-800" role="alert">
                        <p className="font-bold">Registration failed</p>
                        <p className="mt-1">{error}</p>
                    </div>
                )}

                <InputField
                    id="email"
                    label="Email Address"
                    type="email"
                    placeholder="you@example.com"
                    value={email}
                    onChange={(event) => setEmail(event.target.value)}
                    autoComplete="email"
                    required
                />
                <InputField
                    id="password"
                    label="Password"
                    type="password"
                    placeholder="Create a password"
                    value={password}
                    onChange={(event) => setPassword(event.target.value)}
                    autoComplete="new-password"
                    required
                />
                <div>
                    <InputField
                        id="confirmPassword"
                        label="Confirm Password"
                        type="password"
                        placeholder="Enter your password again"
                        value={confirmPassword}
                        onChange={(event) => setConfirmPassword(event.target.value)}
                        autoComplete="new-password"
                        required
                    />
                    <p className="mt-2 text-right text-sm text-muted">
                        Already have an account?{" "}
                        <Link to="/auth/login" className="font-semibold text-[#2563EB] hover:text-[#1D4ED8]">Sign in</Link>
                    </p>
                </div>

                <button type="submit" className="w-full rounded-lg bg-[#2563EB] px-5 py-3.5 text-sm font-semibold text-white shadow-sm transition hover:bg-[#1D4ED8] disabled:cursor-not-allowed disabled:opacity-60" disabled={isSubmitting}>
                    {isSubmitting ? "Creating account..." : "Create account"}
                </button>
            </form>
        </section>
    );
};

export default RegisterPage;
