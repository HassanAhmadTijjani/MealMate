import { useState } from "react";

const SecurityLabPage = () => {
    const [input, setInput] = useState("");

    return (
        <div>
            <h1>MealMate Security Lab</h1>

            <input
                value={input}
                onChange={(event) => setInput(event.target.value)}
                placeholder="Enter something..."
            />

            <div
                dangerouslySetInnerHTML={{
                    __html: input,
                }}
            />
        </div>
    );
};

export default SecurityLabPage;