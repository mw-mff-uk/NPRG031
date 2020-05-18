#!/bin/sh

root="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

mcs "$root/domino.cs" -out:"$root/domino.exe"
cat "$root/input.txt" | mono "$root/domino.exe" --dev
rm "$root/domino.exe"
