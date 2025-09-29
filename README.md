# Test Project: High-Performance Unity Architecture Demo
## A showcase of a custom Entity Component System (ECS) built entirely with Unity’s native toolchain, demonstrating a clean, scalable, and high-performance architecture for modern game development.

## Core Technologies & Patterns
### Custom ECS Implementation
#### Lightweight, data-oriented entity system built from scratch using Unity’s struct-based components and archetype-free design for maximum flexibility.
### VContainer
#### Fast, compile-time dependency injection container for seamless service registration and resolution.
### UniTask
#### Structured, async-friendly initialization pipeline for deterministic and testable startup logic.
### Unity Job System + Burst Compiler
#### Heavy computations (e.g., physics, AI, spatial queries) run in parallel via IJob, accelerated by Burst for near-native performance.
### Service-Oriented Architecture
#### Game logic is decoupled into stateless, reusable services injected via DI.
### Pure C# + Unity 2022+
#### No external packages (besides VContainer and UniTask) — full control over performance and architecture.
