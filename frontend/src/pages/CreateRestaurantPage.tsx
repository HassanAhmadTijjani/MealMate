import CreateRestaurantForm from "../components/CreateRestaurantForm";
import PageHeader from "../components/ui/PageHeader";

const CreateRestaurantPage = () =>
    <section className="mx-auto max-w-3xl px-5 py-14 sm:px-8 sm:py-20">
        <PageHeader eyebrow="Join the directory" title="Add a restaurant" description="Share the essentials so guests can discover and contact the restaurant." />
        <div className="rounded-2xl border border-ink/10 bg-white p-5 shadow-[0_10px_35px_rgba(23,32,29,.05)] sm:p-8">
            <CreateRestaurantForm />
        </div>
    </section>;
export default CreateRestaurantPage;
