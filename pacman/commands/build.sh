#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

csc "$root/main.cs" \
  -r:System.Windows.Forms.dll \
  -r:System.Drawing.dll \
  -out:out/pacman.exe
