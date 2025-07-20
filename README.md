# 🎮 CS Server Info API

A simple and practical API to fetch Counter-Strike server information using `SteamQueryNet`.

---

## 📦 Features

- Retrieve server name, current map, player count, and more
- Display connected player list with name, score, and duration
- Built-in error handling with clear messages

---

## 🚀 Quick Start

### 1. Clone or download the project

```bash
git clone https://github.com/yourusername/cs-server-info-api.git
cd cs-server-info-api
```

### 2. Install required packages

Ensure the following NuGet package is included in your `.csproj` file:

```xml
<PackageReference Include="SteamQueryNet" Version="1.1.1" />
```

Or install it via CLI:

```bash
dotnet add package SteamQueryNet
```

---

## 📡 API Endpoint

### `GET /api/CsServer/info`

Fetch detailed information about a CS server.

#### 📥 Query Parameters:

| Parameter | Type     | Description         |
|-----------|----------|---------------------|
| `ip`      | `string` | Server IP address   |
| `port`    | `ushort` | Server port         |

#### 📤 Sample Response:

```json
{
  "ServerName": "My CS Server",
  "Map": "de_dust2",
  "Players": 12,
  "MaxPlayers": 32,
  "Game": "Counter-Strike",
  "PlayerList": [
    {
      "Name": "Player1",
      "Score": 20,
      "Duration": 512
    },
    {
      "Name": "Player2",
      "Score": 5,
      "Duration": 98
    }
  ]
}
```

#### ❌ Error Example:

```json
{
  "Error": "Failed to connect to server"
}
```

---

## 🧪 Quick Test with curl

```bash
curl -X GET "https://localhost:7053/api/CsServer/info?ip=212.80.8.45&port=19000"
```

---

## 🛠 Controller Overview

```csharp
[HttpGet("info")]
public async Task<IActionResult> GetServerInfo([FromQuery] string ip, [FromQuery] ushort port)
{
    var server = new ServerQuery(ip, port);
    try
    {
        var info = await server.GetServerInfoAsync();
        var players = await server.GetPlayersAsync();

        return Ok(new
        {
            ServerName = info.Name,
            Map = info.Map,
            Players = info.Players,
            MaxPlayers = info.MaxPlayers,
            Game = info.Game,
            PlayerList = players.Select(p => new
            {
                p.Name,
                p.Score,
                Duration = Math.Round(p.Duration)
            })
        });
    }
    catch (Exception ex)
    {
        return BadRequest(new { Error = ex.Message });
    }
}
```

---

## 👤 Author

- 👨‍💻 Developer: Amir Arzani (ZeXi-MoN)
- 📧 Email: zexi.monn@gmail.com

---

## 📄 License

MIT © 2025 — Free to use, modify, and distribute
