#!/bin/sh

root="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

mcs "$root/storekeeper.cs" -out:"$root/storekeeper.exe"
cat "$root/input.txt" | mono "$root/storekeeper.exe" --dev
rm "$root/storekeeper.exe"
