const Table = () => {
    return (
        <mesh
            position={[0, -0.9, 0]}
            receiveShadow
        >
            <cylinderGeometry args={[3.5, 3.5, 0.25, 64]} />

            <meshStandardMaterial
                color="#163a32"
                roughness={0.65}
            />
        </mesh>
    );
};

export default Table;