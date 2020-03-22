for k in 0 1 2 3 4 5 10 25 50 100 150 200 250
do
  mono obchod.exe -k $k > "out/out-${k}.csv"
done

cat out/*.csv > out/out.csv