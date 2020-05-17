#!/bin/sh

root="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

mcs "$root/beasts-in-a-labyrinth.cs" -out:"$root/beasts-in-a-labyrinth.exe"
cat "$root/input.txt" | mono "$root/beasts-in-a-labyrinth.exe" --dev
