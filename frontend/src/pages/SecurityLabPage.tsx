
import FoodViewer from "../components/FoodViewer";

const SecurityLabPage = () => {
    // const [input, setInput] = useState("");

    return (
        <main className="min-h-screen bg-meal-background pt-24">
            <div className="mx-auto max-w-4xl px-6">
                <h1 className="text-3xl font-bold text-meal-primary">
                    Food Viewer Test
                </h1>

                <p className="mt-2 text-meal-muted">
                    Drag the food to rotate it.
                </p>

                <div className="mt-8 h-[500px] overflow-hidden rounded-3xl bg-meal-primary/5">
                    <FoodViewer modelUrl="/models/Burger.glb" />
                </div>
            </div>
        </main>
    );
};

export default SecurityLabPage;