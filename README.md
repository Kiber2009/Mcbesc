# Mcbesc
Minecraft: Bedrock Edition Simple Compiler
> [!WARNING]
> Supports Windows only
# Usage
> [!TIP]
> Add `Mcbesc` to the PATH for ease of use
```bash
Mcbesc [project_file]
```
The default path to project is `.\mcbesc.json`
## Project file
```json
{
    "addons": [
        {
            "name": "main",
            "packs": [
                {
                    "path": "./BP",
                    "jsBuild": {
                        "typescript": true
                    }
                },
                {
                    "path": "./RP"
                }
            ]
        },
        {
            "name": "patch",
            "packs": [
                {
                    "path": "./BP_patch",
                    "jsBuild": {
                        "typescript": true
                    }
                }
            ]
        }
    ],
    "outputDir": "./build"
}
```
- **`addons`:** List of addons
  - **`name`:** Name of compiled file (without `.mcaddon`)
  - **`packs`:** List of packs in addon
  - **`path`:** Path to pack directory
  - **`jsBuild`:** Currently unused
- **`outputDir`:** Path to output directory
