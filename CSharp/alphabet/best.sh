#!/bin/sh

echo ""
echo "Best results:"

for i in 1 2 3 4 5
do
  strokes=$(cat task-two.record.txt | grep -Po "Strokes: \d{5}" | grep -Po "\d{5}" | sort | head -n "$i" | tail -n 1)
  echo -n "${i}.) "
  cat task-two.record.txt | grep "$strokes"
done

iterations=$(cat task-two.record.txt | grep "Grid: |" | wc -l)
echo ""
echo "Total iterations: ${iterations}"

echo ""
echo "Last results:"
cat task-two.record.txt | tail -n 5
