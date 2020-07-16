#!/bin/sh

dir="$( cd "$(dirname "$0")" > /dev/null 2>&1 ; pwd -P )"
root="$dir/.."

csc \
  "$root/src/*.cs" \
  -out:"$root/map-builder.exe"

rm "$root/pacman.tar.gz"
tar -zcvf "$root/pacman.tar.gz" "$root"

rm "$root/documentation.pdf"
mdpdf "$root/readme.md" "$root/documentation.pdf"
