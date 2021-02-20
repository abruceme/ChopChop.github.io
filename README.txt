[Move Integer Values- state name]:[transitions states]
WEAPONLESS MOVE STATES
0- weaponless idle: 4,5,6,7,8,9,21
1- left punch hold: 0,4,5,6
2- up punch hold: 0,4,5,6
3- right punch hold: 0,4,5,6
4- left punch: 0,1
5- up punch: 0,2
6- right punch: 0,3
7- left block/parry: 0,4,5,6,8,9
8- up block/parry: 0,4,5,6,,7,9
9- right block/parry: 0,4,5,6,7,8
WEAPON MOVE STATES
10- weapon idle: 14,15,16,17,18,19,21
11- left slash hold: 0,10,14,15,16
12- up slach hold: 0,10,14,15,16
13- right slash hold: 0,10,14,15,16
14- left slash: 0,10,11,20
15- up slash: 0,10,12,20
16- right slash: 0,10,13,20
17- left block: 0,10,14,15,16,18,19
18- up block: 0,10,14,15,16,17,19
19- right block: 0,10,14,15,16,17,18
MOVING STATES
20- parry reaction: 0
21- running: 0,10 