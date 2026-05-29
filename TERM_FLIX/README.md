# TERM-FLIX

A terminal-first media library manager written in C# / .NET.

TERM-FLIX scans local drives, detects media files, automatically parses them into structured libraries, and provides an interactive terminal UI for browsing and managing content.

Think of it as a terminal-native Plex/Jellyfin experiment with a heavy focus on smart parsing, automation, and local-first workflows.

---

## Features

### Media Scanning

* Recursive drive scanning
* Automatic media discovery
* Duplicate detection
* Broken media detection
* Local-first architecture

### Smart Parsing Engine

TERM-FLIX attempts to automatically recognize:

* Movies
* TV Shows
* Anime
* Seasons
* Episodes
* Release names
* File naming patterns

The parser is heavily regex-driven and designed to handle messy real-world media collections.

Example:

```text
DanMachi/
 ├── Season 1/
 ├── Season 2/
 ├── Season 3/
 └── Season 4/
```

Even inconsistent naming formats are normalized into a structured library whenever possible.

### Interactive TUI

Built using Spectre.Console.

* Keyboard navigation
* Fast terminal UI
* Interactive prompts
* Media browsing
* Resolver workflows

### Resolver System (Planned)

When media cannot be confidently identified, TERM-FLIX can:

* Ask the user for clarification
* Attempt automatic resolution
* Suggest possible matches
* Repair broken entries

### Metadata Integration (Planned)

Future integrations include:

* TVDB
* MyAnimeList
* TMDB
* AniDB

Metadata support will include:

* Episode names
* Descriptions
* Air dates
* Series information

### Watch Tracking (Planned)

* Watch progression tracking
* Resume playback
* Continue watching support
* Viewing history

### Embedded Player Support (Planned)

Currently TERM-FLIX launches the default Windows media player.

Long-term plans include bundling MPV directly into the application for a fully integrated playback experience.

---

## Tech Stack

* C#
* .NET
* Spectre.Console

---

## Current Status

Early development.

The core parsing and scanning systems are currently being built.

---

## Vision

TERM-FLIX is an experiment in building a fully local, terminal-native media ecosystem.

The long-term goal is to create a powerful media manager that feels lightweight, scriptable, fast, and developer-oriented while still being practical for everyday use.

---

## Disclaimer

This project is designed for organizing locally owned media libraries.
