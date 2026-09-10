import { OrbitControls } from "@react-three/drei";
import { Canvas } from "@react-three/fiber";

import Table from "../objects/Table";
import Plate from "../objects/Plate";
import Food from "../objects/Food";

const HeroScene = () => {
    return (
        <Canvas
            shadows
            camera={{
                position: [0, 3, 6],
                fov: 45,
            }}
        >
            {/* Lighting */}
            <ambientLight intensity={1.2} />

            <directionalLight
                position={[4, 6, 4]}
                intensity={2}
                castShadow
            />

            {/* Objects */}
            <Table />
            <Plate />
            <Food />

            {/* Interaction */}
            <OrbitControls
                enableZoom={false}
                enablePan={false}
                enableDamping
            />
        </Canvas>
    );
};

export default HeroScene;