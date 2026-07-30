# Space Shooter

Space Shooter is a networked virtual-reality combat prototype built with Unity. Players can host or discover a game on a local network, select equipment, and fight flying robot enemies in either a timed session or an enemy-kill challenge.

> [!NOTE]
> This documentation is based on a static audit of the repository. The required Unity editor version was not available in the audit environment, so project import, Play Mode, tests, and player builds were not executed.

## Project status

The repository represents a prototype rather than a release-ready game:

- the implemented multiplayer path is LAN host/client only;
- all project-authored scenes are stored under `Assets/Scenes/Debug`;
- no project-specific automated tests, CI pipeline, or custom build scripts are present;
- the project contains a statically identified player-build risk described in [Known limitations](#known-limitations).

## Features

- VR head and hand/controller tracking through XR Interaction Toolkit.
- LAN host/client multiplayer using Netcode for GameObjects and Unity Transport.
- Automatic LAN session discovery over UDP multicast.
- Timed and enemy-kill game session modes.
- Host-controlled flying robot enemies with networked health and session state.
- One- and two-handed weapon support with pistol, machine-gun, and shotgun presets.
- Health, rechargeable energy shield, weapon energy cost, and damage handling.
- Selectable player skins, guns, and life-support systems.
- Local persistence of player and session selections through `PlayerPrefs`.
- World-space UI for health, shield, session state, loading transitions, and multiplayer setup.

## Technology overview

| Component | Configuration |
|---|---|
| Unity | **2022.3.13f1** (`2022.3` LTS) |
| Render pipeline | Universal Render Pipeline **14.0.9** |
| Input | Input System **1.7.0** (transitive dependency); both Input System and legacy input backends are enabled, but project code uses `InputActionReference` |
| XR | XR Interaction Toolkit **2.5.2**, XR Plug-in Management **4.4.0**, Oculus XR Plugin **4.1.1** |
| Networking | Netcode for GameObjects **1.7.1**, Unity Transport **1.4.0** |
| Dependency injection | Zenject **9.2.0** |
| Animation/tweening | DOTween; bundled DLL version is not reliably documented |
| UI | uGUI and TextMesh Pro **3.0.6** |
| Primary configured player target | Android VR, ARM64, IL2CPP, minimum Android API level 29 |

The exact editor version is recorded in [`ProjectSettings/ProjectVersion.txt`](ProjectSettings/ProjectVersion.txt). Open the project with **Unity 2022.3.13f1** to avoid an automatic project upgrade or unintended asset reserialization.

### Important packages

Direct Unity Package Manager dependencies are declared in [`Packages/manifest.json`](Packages/manifest.json), with resolved versions in [`Packages/packages-lock.json`](Packages/packages-lock.json).

| Package | Role in this repository |
|---|---|
| Universal RP 14.0.9 | Active render pipeline in Graphics and Quality settings. |
| Netcode for GameObjects 1.7.1 | Host/client lifecycle, RPCs, network variables, object spawning, ownership, and synchronized scene changes. |
| Unity Transport 1.4.0 | Transport used by `NetworkManager`; resolved transitively through Netcode for GameObjects. |
| Multiplayer Tools 1.1.1 | Installed, but no project-authored code usage was found. |
| XR Interaction Toolkit 2.5.2 | XR origin, controller interaction, locomotion components, and XR UI integration. |
| Input System 1.7.0 | XR controller and UI actions. It is resolved transitively through XR Interaction Toolkit. |
| XR Plug-in Management 4.4.0 | Selects and starts the configured XR loader. |
| Oculus XR Plugin 4.1.1 | The active loader for both Android and Standalone XR configurations. |
| PICO Integration 2.3.4 | Embedded at `Packages/PICO Unity Integration SDK 230`; installed but not selected as an active XR loader. |
| Addressables 1.21.19 | Installed, but there is no `AddressableAssetsData` directory and no project code usage. |
| AI Navigation 1.1.5 | Installed, but no project-authored navigation usage was found. |
| Timeline 1.7.6 | Installed, but no project Timeline assets or code usage were found. |
| Test Framework 1.1.33 | Makes Unity Test Runner available; no project-specific tests are present. |

Zenject and DOTween are vendored under [`Assets/Plugins`](Assets/Plugins), rather than installed through the package manifest.

## Requirements

- Unity Hub.
- Unity Editor **2022.3.13f1**.
- A Git client. Git LFS is **not** configured or required by the current `.gitattributes`.
- For Android builds:
  - Android Build Support;
  - Android SDK & NDK Tools;
  - OpenJDK.
- For functional VR testing, an Oculus-compatible XR runtime and headset. The repository includes an XR Device Simulator sample, but it is not referenced by the project scenes or first-party prefabs.
- For LAN multiplayer testing, at least two game instances or devices on the same IPv4 network with multicast and the required UDP traffic allowed by the firewall.

No backend account, Relay/Lobby configuration, API key, environment file, or connection string is required for the implemented LAN flow.

## Installation

The repository does not document a canonical remote URL. Clone it using the URL provided by the repository owner:

```bash
git clone <repository-url>
cd space_shooter
```

Then:

1. Install Unity **2022.3.13f1** through Unity Hub.
2. Add the Android modules listed in [Requirements](#requirements) if an Android build is needed.
3. In Unity Hub, select **Add project from disk** and choose the repository root—the directory containing `Assets`, `Packages`, and `ProjectSettings`.
4. Open the project and allow Unity Package Manager and the asset database to finish restoring/importing dependencies.
5. Keep the embedded [`Packages/PICO Unity Integration SDK 230`](Packages/PICO%20Unity%20Integration%20SDK%20230) directory in place. It is referenced by the package lock as `com.unity.xr.picoxr`.

The committed package manifest uses only Unity Registry, built-in, and embedded dependencies; no private package registry or Git package authentication is configured.

## Running in the Unity Editor

1. Open [`Assets/Scenes/Debug/MainMenu.unity`](Assets/Scenes/Debug/MainMenu.unity).
2. Confirm that the scene is first in **File > Build Settings**.
3. Start the configured Oculus XR runtime and connect a compatible headset.
4. Enter Play Mode.
5. Use the main menu to select the local multiplayer path:
   - choose **Host** in one instance;
   - choose **Client** in another instance on the same LAN;
   - select the advertised session on the client;
   - configure the session and equipment;
   - start the game from the host.
6. The host performs the synchronized transition to `Fight Scene`.

The repository does not contain a verified non-XR fallback. Headless Play Mode or Play Mode without an active XR runtime may therefore not provide usable player input.

### Local multiplayer network requirements

The LAN discovery and game transport values are hard-coded in [`Assets/Scripts/Network/LocalNetworkInfo.cs`](Assets/Scripts/Network/LocalNetworkInfo.cs):

| Purpose | Value |
|---|---|
| Multicast group | `239.5.5.5` |
| Discovery receive / game transport port | UDP `7779` |
| Host discovery sender port | UDP `7780` |

`HostLogic` obtains the host's first IPv4 address, starts a Netcode host on port `7779`, and broadcasts a JSON `SessionInfo` once per second. `ClientLogic` starts multicast discovery; selecting a session configures Unity Transport with the advertised address and starts a client. Late connections are approved only while the active scene is `MainMenu`.

There is no Relay, Lobby, matchmaking, authentication, NAT traversal, dedicated server, or implemented Internet host/client path.

## Controls

Bindings below come from the action asset referenced by the local XR player prefab: [`Assets/Samples/XR Interaction Toolkit/2.3.2/Starter Assets/XRI Default Input Actions.inputactions`](Assets/Samples/XR%20Interaction%20Toolkit/2.3.2/Starter%20Assets/XRI%20Default%20Input%20Actions.inputactions).

| Action | Keyboard / mouse | XR controller |
|---|---|---|
| Aim / hand pose | Not configured for gameplay | Tracked headset and left/right controller poses |
| Fire equipped left-hand weapon | None | Left trigger |
| Fire equipped right-hand weapon | None | Right trigger |
| XR UI press | Mouse left click is available in the XRI UI map | Left or right trigger |
| Open in-game menu | None | Right controller primary button |
| Exit current session | None | Right controller secondary button |
| UI navigation | `W`, `A`, `S`, `D` or arrow keys | Gamepad/XR navigation depends on the active UI module |

The imported action asset also contains standard XRI locomotion, teleport, grip, select, and thumbstick bindings. Their exact active gameplay behavior was not verified at runtime, so they are not documented here as guaranteed controls.

## Project structure

```text
Assets/
├── Animations/                  # Enemy animation clips and controller
├── Art/                         # Audio, materials, models, sprites, and textures
├── Plugins/                     # DOTween, TextMesh Pro resources, and Zenject
├── Prefabs/                     # Players, enemies, weapons, projectiles, UI, and environment
├── Resources/                   # DOTween and PICO SDK settings
├── Samples/                     # Imported XR Interaction Toolkit samples and input actions
├── Scenes/Debug/                # Project-authored menu, gameplay, and debug scenes
├── ScriptableObjects/Containers # Equipment and skin catalog assets
├── Scripts/                     # First-party runtime code, organized by feature
├── Settings/                    # URP, XR, and XRI settings
└── Terrains/                    # Fight-scene terrain and terrain layers
Packages/
└── PICO Unity Integration SDK 230/  # Embedded PICO Integration 2.3.4 package
ProjectSettings/                 # Unity player, graphics, XR, quality, and build settings
```

Temporary Unity directories (`Library`, `Temp`, `Logs`, `obj`, `Build`, `Builds`, and `UserSettings`) are ignored and are not tracked.

## Architecture

The project is organized around feature folders containing `MonoBehaviour` and `NetworkBehaviour` components. It combines scene-owned controllers, prefab composition, static `PlayerPrefs` access, Unity events, Netcode RPCs/network variables, and a small Zenject binding used for the loading screen.

The main runtime flow is:

1. `MainMenu` owns `NetworkManager`, Unity Transport, the multiplayer menus, session discovery, and session configuration.
2. `HostLogic` or `ClientLogic` starts the selected local network role.
3. `SessionControl` asks the host to load `Fight Scene` through Netcode scene management in `LoadSceneMode.Single`.
4. `PlayersSpawner` creates an owned `PlayerState` network object for every connected client.
5. Each scene's local XR origin is represented by `LocalUser`, which sends tracked body/input data to its owned `PlayerState`.
6. `GameSession` starts the selected challenge on the host and shuts the network down when it ends.

There is no first-party `.asmdef`; project scripts compile into Unity's default `Assembly-CSharp` assembly. Zenject, imported XRI samples, the embedded PICO SDK, and Zenject's bundled tests define their own assemblies.

### Dependency injection

[`Assets/Scripts/Zenject Installers/SceneLogicInstaller.cs`](Assets/Scripts/Zenject%20Installers/SceneLogicInstaller.cs) binds a scene `LoadingScreen` instance. [`SceneLoader`](Assets/Scripts/Game%20Logic/SceneLoader.cs) receives that instance through `[Inject]`. Other first-party systems primarily use serialized references and component lookup rather than container bindings.

## Main systems

### Multiplayer and session discovery

- **Location:** [`Assets/Scripts/Network`](Assets/Scripts/Network)
- **Key classes:** `HostLogic`, `ClientLogic`, `SessionsSharer`, `SessionsLocator`, `SessionsList`, `SessionControl`, `NetworkControl`
- **Responsibilities:** local host/client startup, UDP multicast discovery, session UI, connection approval, synchronized scene changes, and network shutdown.

The `NetworkManager` in `MainMenu` has scene management and connection approval enabled and references [`Assets/DefaultNetworkPrefabs.asset`](Assets/DefaultNetworkPrefabs.asset). The registered network prefabs are the player, flying robot, and simple target.

### Local XR player and replication

- **Location:** [`Assets/Scripts/Player`](Assets/Scripts/Player) and [`Assets/Scripts/User`](Assets/Scripts/User)
- **Key classes:** `LocalUser`, `PlayerState`, `PlayerBodyReferences`, `PlayerInputReferences`, `PlayerHandsInput`
- **Responsibilities:** read local HMD/controller state, find the locally owned network player, upload player configuration, replicate body poses and input, and invoke held weapon interactions.

`PlayerState` uses owner-to-server RPCs and network variables. Remote instances apply replicated head and hand transforms. Player appearance, equipment, name, and life-support selection are initialized on clients through `IInitializer` components.

### Combat, health, and enemies

- **Location:** [`Assets/Scripts/Gun System`](Assets/Scripts/Gun%20System), [`Assets/Scripts/Base`](Assets/Scripts/Base), and [`Assets/Scripts/Enemies`](Assets/Scripts/Enemies)
- **Key classes:** `Gun`, `ShootSystem`, `Bullet`, `HealthSystem`, `FlyingRobot`, `EnemySpawnPoint`
- **Responsibilities:** weapon cooldown and recoil, shield-energy consumption, projectile replication via RPC, damage, health/shield replication, enemy spawning, target detection, aiming, and firing.

Damage and enemy logic are host-controlled. Bullets are local Unity objects reproduced on peers by RPC rather than spawned `NetworkObject` instances.

### Game challenges

- **Location:** [`Assets/Scripts/Game Logic/Session`](Assets/Scripts/Game%20Logic/Session)
- **Key classes:** `GameSession`, `GameTimeChallengeLogic`, `GameEnemyChallengeLogic`, `SessionConfig`
- **Responsibilities:** start the selected host-side session, update synchronized session information, spawn/replenish enemies, and stop the network when the challenge ends.

`SessionType.Time` interprets the configured value as minutes. `SessionType.EnemyKills` interprets it as the required number of destroyed enemies. The menu clamps the value to `5–50`.

### Equipment and customization

- **Location:** [`Assets/Scripts/User/Configuration`](Assets/Scripts/User/Configuration), [`Assets/Scripts/Initializers`](Assets/Scripts/Initializers), and [`Assets/Scripts/SkinSystem`](Assets/Scripts/SkinSystem)
- **Data:** [`Assets/ScriptableObjects/Containers`](Assets/ScriptableObjects/Containers)
- **Responsibilities:** preview and select equipment, then initialize each network player's gun, skin, name, and life-support model/stats.

The committed catalogs contain:

- 3 gun presets: pistol, machine gun, and shotgun;
- 2 life-support systems;
- 2 skins.

### Scene loading and UI

- **Location:** [`Assets/Scripts/UI`](Assets/Scripts/UI) and [`Assets/Scripts/Game Logic/SceneLoader.cs`](Assets/Scripts/Game%20Logic/SceneLoader.cs)
- **Responsibilities:** loading fades/progress, health and shield bars, session information, connected-client count, configuration menus, and the in-game menu.

DOTween drives loading-screen fades, menu fades, and weapon recoil animation.

## Scenes

Build scene order is defined in [`ProjectSettings/EditorBuildSettings.asset`](ProjectSettings/EditorBuildSettings.asset).

| Scene | Purpose | In Build Settings |
|---|---|---|
| [`MainMenu`](Assets/Scenes/Debug/MainMenu.unity) | **Start scene.** Owns network startup, LAN discovery, session/equipment configuration, XR origin, UI, and `NetworkManager`. | Yes, index 0 |
| [`Fight Scene`](Assets/Scenes/Debug/Fight%20Scene.unity) | Networked combat map with player spawning, both challenge logics, enemy spawn points, session UI, and terrain. | Yes, index 1 |
| [`Player`](Assets/Scenes/Debug/Player.unity) | Isolated player/enemy debug sandbox. No first-party scene-loading call targets this scene. | Yes, index 2 |
| [`Spaceship_Destroyer_Control`](Assets/Scenes/Debug/Spaceship_Destroyer/Spaceship_Destroyer_Control.unity) | Standalone spaceship/control debug scene. | No |

Additional `.unity` files under `Assets/Plugins/Zenject`, `Assets/Samples`, and the embedded PICO package are third-party tests or samples and are not product scenes.

The project uses single-scene transitions; no additive scene loading or custom bootstrap scene was found. No first-party `DontDestroyOnLoad` call exists. Network scene lifetime is managed by Netcode's `NetworkManager`, and `NetworkControl` explicitly shuts it down and destroys it when returning to `MainMenu`.

## Rendering and XR configuration

### Universal Render Pipeline

[`Assets/Settings/URP/UniversalRenderPipelineAsset.asset`](Assets/Settings/URP/UniversalRenderPipelineAsset.asset) is assigned in Graphics settings and in the `Ultra` and `Mobile VR` quality levels. Android and Standalone both default to `Mobile VR`.

The active renderer is forward URP with:

- no custom renderer features;
- HDR enabled;
- SRP Batcher enabled;
- no required depth or opaque texture;
- 1x MSAA;
- main-light shadows and no additional-light shadows;
- no project-authored shader or Shader Graph assets.

### XR loaders and devices

[`Assets/Settings/XR/XRGeneralSettings.asset`](Assets/Settings/XR/XRGeneralSettings.asset) configures the **Oculus loader** for Android and Standalone. Oculus settings explicitly enable the Quest and Quest 2 target flags.

The repository also contains:

- embedded PICO Integration 2.3.4;
- a PXR loader asset and PICO settings assets;
- PICO spatial-audio and platform binaries/samples.

However, the PXR loader is not present in the active Android or Standalone loader lists. The PICO platform `appID` field is empty. PICO device/platform support must therefore be treated as **not configured and not verified** in the current project state.

Hand tracking, eye tracking, face tracking, body tracking, and spatial anchors are not enabled in the committed PICO settings. The gameplay code reads controller actions and tracked transforms; it contains no project-authored hand-tracking or eye-tracking integration.

## Configuration and data

### Required committed configuration

| File or directory | Purpose |
|---|---|
| [`Packages/manifest.json`](Packages/manifest.json) | Direct Unity package dependencies. |
| [`Packages/packages-lock.json`](Packages/packages-lock.json) | Exact resolved package graph, including embedded PICO Integration. |
| [`Assets/DefaultNetworkPrefabs.asset`](Assets/DefaultNetworkPrefabs.asset) | Network prefab registration. |
| [`Assets/ScriptableObjects/Containers`](Assets/ScriptableObjects/Containers) | Gun, life-support, and skin catalogs. |
| [`Assets/Samples/XR Interaction Toolkit/2.3.2/Starter Assets`](Assets/Samples/XR%20Interaction%20Toolkit/2.3.2/Starter%20Assets) | Input action asset referenced by the local XR player prefab. |
| [`Assets/Settings/URP`](Assets/Settings/URP) | URP pipeline and renderer data. |
| [`Assets/Settings/XR`](Assets/Settings/XR) | XR loaders and vendor settings. |

There is no `.env`, `StreamingAssets`, custom Android manifest/Gradle template, custom keystore, backend endpoint, or private package registry in the repository.

### Saved data

[`GameData`](Assets/Scripts/Data/GameData.cs) stores these values in Unity `PlayerPrefs`:

- session type and session value;
- player configuration index;
- skin, gun, and life-support indices;
- player name.

The system has no schema version, migration, encryption, cloud synchronization, backup, or explicit `PlayerPrefs.Save` call. Storage location and physical format therefore follow Unity's platform-specific `PlayerPrefs` implementation. If the player-name key is absent, the current code creates it with the default value `text`; no first-party setter or menu event for changing the name was found.

## Building

No custom build pipeline, editor build command, CI workflow, or committed build output is present. The intended procedure is the standard Unity Editor flow.

### Android VR build

1. Install Unity **2022.3.13f1** with Android Build Support, Android SDK & NDK Tools, and OpenJDK.
2. Open the repository and wait for package restoration/import to complete.
3. Open **File > Build Settings**.
4. Select **Android** and choose **Switch Platform**.
5. Confirm that the enabled scenes and order match:
   1. `Assets/Scenes/Debug/MainMenu.unity`
   2. `Assets/Scenes/Debug/Fight Scene.unity`
   3. `Assets/Scenes/Debug/Player.unity`
6. Review **Project Settings > XR Plug-in Management > Android** and keep Oculus selected for the repository's current configuration.
7. Review the committed Android player settings:
   - application identifier: `com.CtrlAltDente.SpaceShooter`;
   - version: `0.1`, bundle version code `1`;
   - minimum API level: `29`;
   - target API level: automatic/highest installed;
   - scripting backend: IL2CPP;
   - architecture: ARM64;
   - custom keystore: disabled.
8. For local development, choose **Development Build** if needed and click **Build** or **Build And Run**.
9. For a distributable release, configure an organization-controlled Android keystore outside the repository. Do not commit keystore files or passwords.

### Standalone

Standalone XR settings also select the Oculus loader, but the repository contains no platform-specific standalone build procedure or evidence of a validated Windows, Linux, or macOS player build. Treat Standalone as an Editor/testing configuration until it is explicitly verified.

### Build verification status

A build was not executed during this audit because Unity 2022.3.13f1 was not installed in the environment. In addition, the `GameCloser` issue below should be addressed before assuming that a player build will compile.

## Testing

Unity Test Framework 1.1.33 is installed. No project-specific Edit Mode, Play Mode, integration, or smoke tests were found, and there are no project test assemblies or test scenes identified as product test coverage.

Zenject includes its own upstream unit/integration test sources and test scenes under `Assets/Plugins/Zenject/OptionalExtras`. Those tests validate the vendored dependency, not Space Shooter gameplay.

To inspect or run available tests:

1. Open the project with Unity 2022.3.13f1.
2. Open **Window > General > Test Runner**.
3. Use the **EditMode** and **PlayMode** tabs as applicable.

No repository-specific batch-mode test command is documented or configured. Tests were not run during this audit.

## Known limitations

- **Potential player-build compile blocker:** [`Assets/Scripts/Game Logic/GameCloser.cs`](Assets/Scripts/Game%20Logic/GameCloser.cs) imports `UnityEditor` outside an `#if UNITY_EDITOR` guard. Although `EditorApplication` usage is guarded, the unconditional import normally needs to be excluded from player compilation. This was identified statically and not confirmed with an actual build.
- `ClientLogic.SelectInternetClient()` and `HostLogic.SelectInternetHost()` are empty. The Internet multiplayer menu does not have an implemented network path.
- Multiplayer uses direct local IPv4 addresses, fixed UDP ports, and multicast discovery. It has no Relay, NAT traversal, reconnect flow, session authentication, or dedicated-server support.
- The PICO SDK is embedded, but the PXR loader is inactive and the PICO platform App ID is unset. PICO execution and platform services are not configured.
- The active XR Interaction Toolkit package is 2.5.2, while the first-party XR player prefab references a committed Starter Assets input action asset under the older `2.3.2` sample directory. Removing or reimporting samples can break those serialized references.
- The XR Device Simulator 2.5.2 sample exists in the repository, but no project-scene or first-party-prefab reference to it was found.
- `Player.unity` is enabled in Build Settings even though no first-party code path loads it.
- `Spaceship_Destroyer_Control.unity` is outside Build Settings and contains serialized script GUIDs that do not resolve to tracked `.meta` files. Treat it as an incomplete legacy/debug scene.
- No project-specific automated tests, CI, build automation, save-data migrations, or explicit compatibility guarantees are present.

## Version control notes

- `.gitignore` excludes standard Unity generated directories, IDE files, player builds, APK/AAB files, and generated Addressables content.
- No generated Unity directory is currently tracked.
- `.gitattributes` only enables automatic text normalization; it defines no Git LFS patterns.
- Binary art, audio, model, and plugin files are stored directly in Git.

## License

No project-level `LICENSE` file is present.

> The project license is not specified. By default, this does not grant permission to use, modify, or distribute the code.

Third-party components remain subject to their own terms:

- Zenject 9.2.0 includes [`Assets/Plugins/Zenject/LICENSE.txt`](Assets/Plugins/Zenject/LICENSE.txt).
- DOTween includes a bundled [`readme.txt`](Assets/Plugins/Demigiant/DOTween/readme.txt) that points to the vendor's license information.
- Unity packages and the embedded PICO SDK are distributed under their respective vendor terms. No standalone PICO license file was found in the embedded package, so redistribution terms require separate verification.

The Unity Player Settings identify the company name as **CtrlAltDente**. No individual authorship or contributor information is documented in the repository.
