# Mcbesc
Minecraft: Bedrock Edition Simple Compiler
> [!WARNING]
> Supports Windows only

# Usage
> [!TIP]
> Add Mcbess to the PATH for ease of use
```bash
Mcbesc [project_file]
```
The standard path to the project is `.\mcbesc.json`
## Project file
```json
{
    "Packs": [
        "./Pack1",
        "./Pack2"
    ],
    "BuildDir": "./build",
    "BuildName": "my_addon"
}
```
- **Packs:** List of paths to packs
- **BuildDir:** Path to output directory
- **BuildName:** Name of compiled file (without `.mcaddon`)
