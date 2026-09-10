const Food = () => {
    return (
        <group position={[0, 0.2, 0]}>
            {/* Main food */}
            <mesh position={[0, 0.35, 0]} castShadow>
                <sphereGeometry args={[0.65, 32, 24]} />
                <meshStandardMaterial
                    color="#d96c4a"
                    roughness={0.8}
                />
            </mesh>

            {/* Side 1 */}
            <mesh position={[-0.65, 0.15, 0.2]} castShadow>
                <sphereGeometry args={[0.2, 24, 16]} />
                <meshStandardMaterial
                    color="#4f7a6a"
                    roughness={0.9}
                />
            </mesh>

            {/* Side 2 */}
            <mesh position={[0.65, 0.15, 0.2]} castShadow>
                <sphereGeometry args={[0.2, 24, 16]} />
                <meshStandardMaterial
                    color="#4f7a6a"
                    roughness={0.9}
                />
            </mesh>

            {/* Garnish */}
            <mesh position={[0, 0.8, 0.15]} castShadow>
                <sphereGeometry args={[0.12, 20, 16]} />
                <meshStandardMaterial
                    color="#e5b85c"
                    roughness={0.6}
                />
            </mesh>
        </group>
    );
};

export default Food;