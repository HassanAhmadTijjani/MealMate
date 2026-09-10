import { Canvas, useFrame } from "@react-three/fiber";
import { OrbitControls, useGLTF } from "@react-three/drei";
import * as THREE from "three";
import { useRef } from "react";

type FoodModelProps = {
    modelUrl: string;
};


const FoodModel = ({ modelUrl }: FoodModelProps) => {
    const { scene } = useGLTF(modelUrl);
    const foodRef = useRef<THREE.Mesh>(null);
    useFrame(() => {
        if (!foodRef.current) return;

        foodRef.current.rotation.x += 0.001;
        foodRef.current.rotation.y += 0.01;
    });

    return (
        <mesh ref={foodRef}>

            <primitive object={scene} scale={1} />
        </mesh>
    );
};

type FoodViewerProps = {
    modelUrl: string;
};

const FoodViewer = ({ modelUrl }: FoodViewerProps) => {
    
    return (
        <div className="h-full w-full">
            <Canvas
                shadows
                camera={{
                    position: [0, 0, 4],
                    fov: 45,
                }} 
            >

                <ambientLight intensity={5} />

                <directionalLight
                    position={[3, 5, 3]}
                    intensity={2}
                    castShadow
                />

                <FoodModel modelUrl={modelUrl} />

                <OrbitControls
                    enableZoom={false}
                    enablePan={false}
                    enableDamping
                />
            </Canvas>
        </div>
    );
};

export default FoodViewer;