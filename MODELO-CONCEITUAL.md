# Modelo conceitual — Locadora de Veículos

```text
FABRICANTE (1) ─────────── (N) VEICULO (1) ─────────── (N) ALUGUEL
                               │                              │
                               │                              │
                         (N)   │                              │   (N)
CATEGORIA  (1) ────────────────┘                              │
                                                              │
                                                CLIENTE (1) ──┘
```

## Entidades e chaves

- **Fabricante**: `FabricanteId` (PK).
- **Categoria**: `CategoriaId` (PK).
- **Cliente**: `ClienteId` (PK).
- **Veiculo**: `VeiculoId` (PK), `FabricanteId` (FK) e `CategoriaId` (FK).
- **Aluguel**: `AluguelId` (PK), `ClienteId` (FK) e `VeiculoId` (FK).

## Relacionamentos

- Um fabricante possui muitos veículos; cada veículo pertence a um fabricante.
- Uma categoria possui muitos veículos; cada veículo pertence a uma categoria.
- Um cliente possui muitos aluguéis; cada aluguel pertence a um cliente.
- Um veículo pode participar de muitos aluguéis; cada aluguel corresponde a um veículo.
