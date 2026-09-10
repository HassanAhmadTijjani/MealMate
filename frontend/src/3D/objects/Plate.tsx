const Plate = () => {
    return (
        <mesh
            rotation={[-Math.PI / 2, 0, 0]}
            receiveShadow
            castShadow
        >
            <cylinderGeometry args={[1.6, 1.6, 0.15, 64]} />

            <meshStandardMaterial
                color="#fffcf6"
                roughness={0.35}
            />
        </mesh>
    );
};

export default Plate;