#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

csc \
  "$root/src/utils/*.cs" \
  "$root/src/*.cs" \
  -r:System.Windows.Forms.dll \
  -r:System.Drawing.dll \
  -out:out/pacman.exe
