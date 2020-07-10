#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

csc \
  "$root/src/main.cs" \
  "$root/src/game.cs" \
  "$root/src/utils/linked-list.cs" \
  -r:System.Windows.Forms.dll \
  -r:System.Drawing.dll \
  -out:out/pacman.exe
