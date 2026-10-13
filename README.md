# ✈️ Paper Flights

[![Unity 6](https://img.shields.io/badge/Unity-6-black?style=flat&logo=unity)](https://unity.com/)
[![C#](https://img.shields.io/badge/Language-C%23-blue?style=flat&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![itch.io](https://img.shields.io/badge/Play-itch.io-fa5c5c?style=flat&logo=itch.io)](https://tonylie.itch.io/paper-flights)

> **Paper Flights** is a fast-paced, hand-drawn 2D arcade shoot 'em up (shmup) built in Unity 6. Navigate dynamic school and office environments, dodge hazards, leverage a stackable power-up system, and survive until the ultimate boss showdown!

---

## 🎮 Play & Demonstration

* **Play in Browser:** [Experience Paper Flights live on itch.io](https://tonylie.itch.io/paper-flights)

> *[Insert a high-resolution GIF or short video clip here showing core combat, power-up pickup, and boss encounter]*

---

## ✨ Core Features

* **Dynamic Environments:** Dual-layer background architecture featuring precision world-space tracking for a seamless infinite scroll effect.
* **Stackable Power-Up System:** Item drop system supporting *Triple Shot*, *Shields*, *Homing Rockets*, and a coin-pulling *Magnet* with live multi-line timer UI tracking.
* **Wave-Based Enemy Spawning:** Configurable randomized enemy spawn manager scaling into a scripted boss encounter with synchronized health bars and multi-directional attack patterns.
* **Persistent Economy & Shop:** Integrated `PlayerPrefs` data persistence allowing players to collect currency, track high scores, and unlock alternate plane skins via a custom shop menu.
* **Decoupled Audio Architecture:** Centralized persistent singleton `AudioManager` managing master volume preferences, UI feedback, and positioned 2D spatial audio.

---

## 🏗️ Technical Architecture & Design Patterns

Designed with **clean code principles**, modularity, and separation of concerns to ensure high maintainability and performance:

* **Design Patterns:** Implemented persistent Singletons (`AudioManager`, `EnemySpawner`) and decoupled event-driven UI updates to prevent tight coupling between game systems.
* **Performance Optimization:** Frame-rate independent movement, cached component references to eliminate redundant `GetComponent` calls in `Update()`, and rigorous null-safety checks.
* **Modern Input System:** Integrated Unity's modern Input System for responsive, extensible keyboard arcade controls.
* **WebGL Deployment Pipeline:** Configured custom responsive Canvas Scalers (16:9 target ratio) and Minimal WebGL templates to ensure a clean, borderless portfolio presentation on itch.io.

---

## 💡 Engineering Highlights & Problem Solving

* **UI & Resolution Consistency across Platforms:** Addressed WebGL iframe scaling hurdles by synchronizing Unity’s active Build Profile canvas width/height (`960x540`) with browser viewport settings, avoiding edge-cropping while retaining high-DPI font sharpness via Unity's *Scale With Screen Size* mode.
* **Decoupled Event Handling:** Utilized C# events for decoupled communication between player health changes, score trackers, and UI controller updates.

---

## ⌨️ Controls

| Action | Key / Input |
| :--- | :--- |
| **Movement** | Arrow Keys (`Left`, `Right`, `Up`, `Down`) |
| **Shoot** | `Z` Key / Spacebar |
| **Pause / Settings** | `Escape` Key |

---

## 🚀 Getting Started & Running Locally

### Run From Source
1. Clone this repository or download it as a ZIP.
2. Open **Unity Hub** and select **Add project from disk**.
3. Load the project using **Unity 6** (or a compatible LTS version).
4. Open the `Main Menu Scene` located in `Assets/_Project/Scenes/` and hit **Play**!

---

## 📂 Project Structure

```text
Assets/
├── _Project/
│   ├── Scripts/
│   │   ├── Managers/     # Core systems (AudioManager, Game/Pause/Score/Shop Managers)
│   │   ├── Player/       # Player movement, health, power-ups, and sprite loading
│   │   ├── Enemies/      # Enemy behaviors, spawner, bullets, and boss mechanics
│   │   └── UI/           # UI controllers, health displays, and button SFX handlers
│   ├── Prefabs/          # Reusable game objects (Enemies, Bullets, Pickups, UI elements)
│   └── Scenes/           # Main menu and gameplay scene files
