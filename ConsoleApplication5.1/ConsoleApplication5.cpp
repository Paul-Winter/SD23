#include <iostream>


union number { unsigned int number_digit;char number_sumbol[9]; }   car_number__1;
struct car { int rgb[3];std::string model;number car_number; }   car__1;


bool digital_1___string_0;

void print_car(car this_car, bool digit) { std::cout << this_car.rgb[0] << ' ' << this_car.rgb[1] << ' ' << this_car.rgb[2] << '\n' << this_car.model << '\n';if (digital_1___string_0) { std::cout << this_car.car_number.number_digit << '\n'; } else { std::cout << this_car.car_number.number_sumbol << '\n'; } }

car new_car_stats(car this_car, bool digit) { std::cin >> this_car.rgb[0] >> this_car.rgb[1] >> this_car.rgb[2] >> this_car.model;if (digital_1___string_0) { std::cin >> this_car.car_number.number_digit; } else { std::cin >> this_car.car_number.number_sumbol; }return this_car; }



void find_car_by_number(car cars_list[], int all_cars, bool list[], int number_int) { for (int find_number = 0;find_number < all_cars;find_number++) { if (cars_list[find_number].car_number.number_digit == number_int) { std::cout << cars_list[find_number].model << '\n'; } } }
void find_car_by_number(car cars_list[], int all_cars, bool list[], char number_char[]) { for (int find_number = 0;find_number < all_cars;find_number++) { if (cars_list[find_number].car_number.number_sumbol == number_char) { std::cout << cars_list[find_number].model << '\n'; } } }

void print_all_cars(car cars_list[], int all_cars, bool list[]) { for (int this_number = 0;this_number < all_cars;this_number++) { std::cout << cars_list[this_number].rgb[0] << ' ' << cars_list[this_number].rgb[1] << ' ' << cars_list[this_number].rgb[2] << '\n' << cars_list[this_number].model << '\n';if (list[this_number]) { std::cout << cars_list[this_number].car_number.number_digit << "\n\n"; } else { std::cout << cars_list[this_number].car_number.number_sumbol << "\n\n"; } } }

void new_stats_for_this_car(car cars_list[], int this_car_number, bool list[], bool add_number_1__add_sumbol_0) { std::cin >> cars_list[this_car_number].rgb[0] >> cars_list[this_car_number].rgb[1] >> cars_list[this_car_number].rgb[2] >> cars_list[this_car_number].model;if (add_number_1__add_sumbol_0) { std::cin >> cars_list[this_car_number].car_number.number_digit; } else { std::cin >> cars_list[this_car_number].car_number.number_sumbol; }list[this_car_number] = add_number_1__add_sumbol_0; }




enum types_life { bird = 0, cattle, human };
union statistic { float speed_fly;bool artiodactyl;int iq; };
struct live { float speed_walk;types_life life_type;int rgb[3];statistic life_stat; };
void print_this_live(live print_live) { std::cout << print_live.speed_walk << '\n';if (print_live.life_type == 2) { std::cout << "human"; } else if (print_live.life_type) { std::cout << "cattle"; } else { std::cout << "bird"; }std::cout << '\n' << print_live.rgb[0] << ' ' << print_live.rgb[1] << ' ' << print_live.rgb[2] << '\n';if (print_live.life_type == 2) { std::cout << "iq:" << print_live.life_stat.iq; } else if (print_live.life_type) { std::cout << "artiodactyl:" << print_live.life_stat.artiodactyl; } else { std::cout << "speed_fly:" << print_live.life_stat.speed_fly; }std::cout << '\n'; }
live new_stat_for_live(live this_live) { int choise;std::cin >> this_live.speed_walk >> choise;if (choise == 2) { this_live.life_type = human; } else if (choise) { this_live.life_type = cattle; } else { this_live.life_type = bird; }std::cin >> this_live.rgb[0] >> this_live.rgb[1] >> this_live.rgb[2];if (choise == 2) { std::cin >> this_live.life_stat.iq; } else if (choise) { std::cin >> this_live.life_stat.artiodactyl; } else { std::cin >> this_live.life_stat.speed_fly; }return this_live; }

void print_all_lives(live live_list[], int all_lives, int list[]) { for (int this_number = 0;this_number < all_lives;this_number++) { std::cout << live_list[this_number].speed_walk << '\n';if (live_list[this_number].life_type == bird) { std::cout << "bird"; } else if (live_list[this_number].life_type == cattle) { std::cout << "cattle"; } else { std::cout << "human"; }std::cout << '\n';std::cout << live_list[this_number].rgb[0] << ' ' << live_list[this_number].rgb[1] << ' ' << live_list[this_number].rgb[2] << '\n';if (list[this_number] == 2) { std::cout << live_list[this_number].life_stat.iq << "\n"; } else if (list[this_number]) { std::cout << live_list[this_number].life_stat.artiodactyl << "\n"; } else { std::cout << live_list[this_number].life_stat.speed_fly << '\n'; }std::cout << '\n'; } }
void stat_for_live_in_list(live live_list[], int this_creature, int list[]) { int t;std::cin >> live_list[this_creature].speed_walk >> t;list[this_creature] = t;if (t == 2) { live_list[this_creature].life_type = human; } else if (t) { live_list[this_creature].life_type = cattle; } else { live_list[this_creature].life_type = bird; }std::cin >> live_list[this_creature].rgb[0] >> live_list[this_creature].rgb[1] >> live_list[this_creature].rgb[2];if (t == 2) { std::cin >> live_list[this_creature].life_stat.iq; } else if (t) { std::cin >> live_list[this_creature].life_stat.artiodactyl; } else { std::cin >> live_list[this_creature].life_stat.speed_fly; } }
void find_by_stats(live live_list[], int all_lives, int iq_need) { for (int this_number = 0;this_number < all_lives;this_number++) { if (live_list[this_number].life_type == human && live_list[this_number].life_stat.iq == iq_need) { std::cout << "human " << this_number << '\n'; } } }
void find_by_stats(live live_list[], int all_lives, bool artiodacty_need) { for (int this_number = 0;this_number < all_lives;this_number++) { if (live_list[this_number].life_type == cattle && live_list[this_number].life_stat.artiodactyl == artiodacty_need) { std::cout << "cattle " << this_number << '\n'; } } }
void find_by_stats(live live_list[], int all_lives, float speed_fly_need) { for (int this_number = 0;this_number < all_lives;this_number++) { if (live_list[this_number].life_type == bird && live_list[this_number].life_stat.speed_fly == speed_fly_need) { std::cout << "bird " << this_number << '\n'; } } }


int main() {


	car_number__1.number_digit = 8845932 % 100000;

	car__1 = { {1,2,3},"good",car_number__1.number_digit };

	digital_1___string_0 = true;

	print_car(car__1, digital_1___string_0);

	digital_1___string_0 = false;
	car__1 = new_car_stats(car__1, digital_1___string_0);

	print_car(car__1, digital_1___string_0);

	digital_1___string_0 = true;
	car__1 = new_car_stats(car__1, digital_1___string_0);

	print_car(car__1, digital_1___string_0);








	number default_number;
	default_number.number_digit = 0 % 100000;

	car default_car = { {0,0,0},"default",default_number.number_digit };

	car cars[10];
	bool digital_1__string_0___list[10];

	for (int t = 0;t < 10;t++) { cars[t] = default_car;digital_1__string_0___list[t] = 1; }



	print_all_cars(cars, 10, digital_1__string_0___list);

	new_stats_for_this_car(cars, 4, digital_1__string_0___list, 0);

	print_all_cars(cars, 10, digital_1__string_0___list);




	live new_live;

	 new_stat_for_live(new_live);

	print_this_live(new_live);

	live lives[10];

	live default_live = { 0,bird,{0,0,0},0 };
	int bird_0__cattle_1__human_2[10];
	for (int t = 0;t < 10;t++) { lives[t] = default_live;bird_0__cattle_1__human_2[t] = 0; }
	print_all_lives(lives, 10, bird_0__cattle_1__human_2);
	stat_for_live_in_list(lives, 3, bird_0__cattle_1__human_2);
	print_all_lives(lives, 10, bird_0__cattle_1__human_2);
	find_by_stats(lives, 10, 0.0f);


}