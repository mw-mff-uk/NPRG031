#!/bin/sh

dir=$( dirname "$(realpath "$0")" )
root="$dir/.."

mono "$root/out/pacman.exe"
