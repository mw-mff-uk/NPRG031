#!/bin/sh

dir="$( cd "$(dirname "$0")" > /dev/null 2>&1 ; pwd -P )"
root="$dir/.."

csc \
  "$root/src/utils/*.cs" \
  "$root/src/avatars/*.cs" \
  "$root/src/events/*.cs" \
  "$root/src/*.cs" \
  -out:"$root/pacman.exe"
