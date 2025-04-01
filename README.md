# 🌑 Pacman - COSC416 Game Jam Project

Welcome to our Game Jam entry — a retro **Pac-Man clone** with a twist of darkness, tension, and style. Built with **Unity 6** and designed to run directly in your browser via **WebGL**, this version recreates the classic arcade formula with a modern gameplay mechanic.

🎮 **[▶ Play Now on Itch.io](https://chasew.itch.io/pacman-in-unity)**  
*(Playable instantly — no download needed!)*

---

## Made By
- Stuart McGorman (Pellets, Darkness Machanic, HUD)
- Preston Melvin (Map design, Shaders Arcade Style, Sounds)
- Jordan Truong (Ghost Chase & Scatter, Ghost animations, Indiviual Ghost Behaviours)
- Chase Winslow (Pac-man movement and animations, Play button, Game Over Screen)
 
---

## 🔦 The Twist: Darkness Is Your Enemy

In this version of Pac-Man:

- The **screen starts illuminated**, but **darkness slowly creeps in** from the edges.
- Eating **pellets** temporarily **pushes back the darkness**, widening your view.
- If you don't eat fast enough, your screen will shrink into **total darkness**!
- Luckily, **pellets regenerate** over time, letting you reclaim visibility.
- The pressure is on — keep moving, keep eating, and survive the dark!

---

## 🎮 Gameplay Highlights

- ✅ Classic Pac-Man movement and maze mechanics  
- 👻 Ghosts with behavior inspired by the originals (Blinky, Pinky, Inky, Clyde)  
- 🌌 Strategic darkness and light system  
- 🟣 CRT shader filter for authentic retro visual style  
- 🔊 Classic-inspired SFX and audio system  
- 📺 HTML5 / WebGL build – **play directly in browser**

---

## 🛠️ Tech Stack

- **Engine:** Unity 6.0.0 (2024)
- **Language:** C#
- **Build Target:** WebGL (Itch.io-friendly)
- **Graphics:** Aseprite, Photoshop
- **Audio Tools:** Audacity, Unity AudioManager

---

## 🗂️ Project Folder Structure


- `Assets/Animations/`        # Character and object animations
- `Assets/CRT_Filter/`         # Retro CRT visual effects
- `Assets/Fonts/ `             # In-game retro fonts
- `Assets/Physics/`            # Physics-related components
- `Assets/Prefabs/`            # Ghosts, Pacman, pellets, UI prefabs
- `Assets/Scenes/ `            # Main game scene(s)
- `Assets/Scripts/`            # All gameplay logic (Pacman, Ghosts, Darkness, Pellets, etc.)
- `Assets/Settings/ `          # Project-specific configuration
- `Assets/Sprites/    `        # Maze tiles, characters, visual assets

---

## 🧠 How to Play

- Use **arrow keys** or **W,A,S,D** to move
- Collect **pellets** to increase your field of view
- Avoid ghosts — unless you’ve grabbed a **Power Pellet**
- Stay alive, rack up a high score, and don’t let the darkness consume you!

---

## 🧑‍💻 Credits

Made with 💛 by the COSC416 Game Jam Team  
- Gameplay & Mechanics: Darkness mechanic, pellet regen, ghost AI  
- Design & Art: Retro look, UI, sprites, and level layout from ITCH.io with free use content
- Sound & Music: Classic arcade-style sfx with a modern twist  
- Programming: Unity C# (Scripts: `Darkness.cs`, `Pellet.cs`, `GameManager.cs`, etc.)

---

## 🚀 Running Locally (Optional)

Want to run it in Unity?

1. Open in **Unity 6.0.0f1 or later**
2. Load the `Main` scene
3. Hit **Play**
4. To build for HTML5: `File > Build Settings > WebGL > Build`

---

# Video demo on YouTube
https://www.youtube.com/watch?v=pyO4LAcYlPk

(Audio is a bit buggy on screen recording)

---


© 2025 COSC416 Game Jam Team | Retro GameJam 🎉
