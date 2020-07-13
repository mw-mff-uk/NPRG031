#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

csc \
  "$root/src/utils/*.cs" \
  "$root/src/avatars/*.cs" \
  "$root/src/events/*.cs" \
  "$root/src/*.cs" \
  -out:out/pacman.exe
