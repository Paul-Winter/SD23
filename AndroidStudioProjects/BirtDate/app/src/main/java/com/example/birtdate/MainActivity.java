package com.example.birtdate;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.Date;
import java.time.LocalDate;
import java.time.Period;


public class MainActivity extends AppCompatActivity {

    private TextView txtProsba;
    private EditText editTxtDate;
    private Button getDataBtn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
        txtProsba = findViewById(R.id.txtProsba);
        editTxtDate = findViewById(R.id.editTxtDate);
        getDataBtn = findViewById(R.id.getDataBtn);

    }

    public void getDataBtnClick(View view)
    {
        String date = editTxtDate.getText().toString();

        if (!date.isEmpty())
        {
            editTxtDate.setVisibility(View.GONE);
            getDataBtn.setVisibility(View.GONE);
            txtProsba.setText("date");

            LocalDate today = LocalDate.now();
            LocalDate targetDate = LocalDate.of(2025, 12, 31);


        }
    }
}