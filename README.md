# Simple 3D Shooting Game — Unity

A beginner-friendly first-person shooter prototype built with Unity and C#.

## Features

- First-person movement: walk, run, jump, and mouse look
- Projectile shooting with reload
- Destructible targets
- Score system
- Simple UI score display

## Requirements

- Unity 2022 LTS or newer (Unity 6 recommended)
- 3D URP project template

## Setup

1. Create a new Unity project using the **3D URP** template.
2. Copy the `Assets/Scripts` folder into your Unity project.
3. Create a Ground plane and add several cubes as obstacles.
4. Create a Player capsule:
   - Add `CharacterController`
   - Add `PlayerController`
   - Add `Shooting`
   - Create a child empty object named `CameraHolder`
   - Place the Main Camera under `CameraHolder`
   - Create a child empty object named `GunMuzzle`
5. Create a Bullet prefab:
   - Create a small Sphere
   - Add a `Rigidbody`
   - Optionally disable `Use Gravity`
   - Add the `Bullet` script
   - Save it as a prefab
   - Assign it to `bulletPrefab` on the Player
6. Create targets:
   - Add cubes or capsules
   - Add the `Target` script
   - Ensure each has a Collider
7. Create UI:
   - Create a Canvas
   - Add a Text object named `ScoreText`
   - Create an empty GameObject named `GameManager`
   - Add the `GameManager` script
   - Assign `ScoreText` to its `scoreText` field

## Controls

| Input | Action |
|---|---|
| W A S D | Move |
| Mouse | Look |
| Left Mouse Button | Shoot |
| Shift | Run |
| Space | Jump |
| R | Reload |

## Publish to GitHub

```bash
git init
git add .
git commit -m "Initial Unity 3D shooter prototype"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git
git push -u origin main
```

Do not upload the `Library`, `Temp`, `Obj`, or `Builds` folders; the included `.gitignore` excludes them.

## License

MIT License.
