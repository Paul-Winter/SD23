#include<iostream>

int** add_1_d(int y, int x, int** old, int new_layer[], int for_new) {
	int** _2_d_new = new int* [y + 1];
	for (int y_layer = 0, add_old_layer = 0;y_layer < y + 1;y_layer++) {
		_2_d_new[y_layer] = new int[x];
		if (y_layer != for_new) { _2_d_new[y_layer] = old[add_old_layer];add_old_layer++; }
		else { for (int add_x = 0;add_x != x;add_x++) { _2_d_new[y_layer][add_x] = new_layer[add_x]; } }
	}





	return _2_d_new;
}


int main() {
	int y = 2;int x = 2;
	int** _2_d = new int* [y];
	for (int y_layer = 0;y_layer < y;y_layer++) { _2_d[y_layer] = new int[x]; }


	_2_d[0][0] = 1;_2_d[0][1] = 2;_2_d[1][0] = 3;_2_d[1][1] = 4;


	for (int layer_y = 0;layer_y < y;layer_y++)
	{
		for (int layer_x = 0;layer_x < x;layer_x++)
		{
			std::cout << _2_d[layer_y][layer_x];
		}std::cout << '\n';
	}
	std::cout << '\n';

	int la[2] = { 9,10 };

	int** final = add_1_d(y, x, _2_d, la, 1);

	for (int layer_y = 0;layer_y < y + 1;layer_y++)
	{
		for (int layer_x = 0;layer_x < x;layer_x++)
		{
			std::cout << final[layer_y][layer_x] << ' ';
		}std::cout << '\n';
	}



	for (int del_y_layer = 0; del_y_layer < y; del_y_layer++) {
		delete[] _2_d[del_y_layer];
	}
	delete[] _2_d;

	for (int del_y_layer = 0; del_y_layer < y + 1; del_y_layer++) {
		delete[] final[del_y_layer];
	}
	delete[] final;
}