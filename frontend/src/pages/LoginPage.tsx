import React, { useState } from 'react'
// import { login } from '../services/AuthService';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import InputField from '../components/ui/InputField';
import PageHeader from '../components/ui/PageHeader';
import { login } from '../services/AuthService';

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const navigate = useNavigate();
    const location = useLocation();
    const registrationMessage = (location.state as { registrationMessage?: string } | null)?.registrationMessage;

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setError(null);
        setIsSubmitting(true);
        // console.log('Base URL:', API_BASE_URL);
        try {
            await login({ email, password });
            // const token = await login({ email, password });
            // localStorage.setItem("accessToken", token);
            // console.log(`JWT recieved, ${token}`);
            navigate("/");
        } catch(e) {
            setError(e instanceof Error ? e.message : "Login failed. Please try again.");

        } finally {
            setIsSubmitting(false);
        }
    }
    return (
        <section className='mx-auto max-w-3xl px-5 py-14 sm:px-8 sm:py-20'>
            <PageHeader eyebrow='Welcome back' title='Login to continue' />

            <form onSubmit={handleSubmit} className='space-y-8'>
                {registrationMessage && <div className="rounded-lg border border-emerald-200 bg-emerald-50 p-4 text-sm text-emerald-800" role="status">
                    {registrationMessage}
                </div>}
                {error && <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-800" role="alert">
                    <p className="font-bold">Login failed</p>
                    <p className="mt-1">{error}</p>
                </div>}
                <InputField id="email" label="Email Address" type="email" placeholder="you@example.com" value={email} onChange={(event: { target: { value: React.SetStateAction<string>; }; }) => setEmail(event.target.value)} autoComplete="email" required />
                <div>
                    <InputField id="password" label="Password" type="password" placeholder="Enter your password" value={password} onChange={(event: { target: { value: React.SetStateAction<string>; }; }) => setPassword(event.target.value)} autoComplete="current-password" required />
                    <div className="mt-2 flex justify-between items-center">
                        <button type="button" className="text-sm font-semibold text-[#2563EB] hover:text-[#1D4ED8]">Forgot password?</button>
                        <Link to="/auth/register" className="text-sm font-semibold text-[#2563EB] hover:text-[#1D4ED8]">Create an account</Link>
                    </div>
                </div>
                <button type="submit" className=" mx-auto w-full rounded-lg bg-[#2563EB] px-5 py-3.5 text-sm font-semibold text-white shadow-sm transition hover:bg-[#1D4ED8]" disabled={isSubmitting}>
                    {isSubmitting ? "Logging In..." : "Login"}
                </button>
            </form>
        </section>
    )
}

export default LoginPage
