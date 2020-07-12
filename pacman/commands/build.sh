#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

csc \
  "$root/src/utils/*.cs" \
  "$root/src/avatars/*.cs" \
  "$root/src/points/*.cs" \
  "$root/src/keyboard-handler.cs" \
  "$root/src/map.cs" \
  "$root/src/game.cs" \
  "$root/src/main.cs" \
  -r:System.Windows.Forms.dll \
  -r:System.Drawing.dll \
  -out:out/pacman.exe
