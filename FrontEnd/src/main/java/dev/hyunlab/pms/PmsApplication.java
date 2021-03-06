package dev.hyunlab.pms;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.jdbc.DataSourceAutoConfiguration;

/**
 * @see https://lemontia.tistory.com/586
 */
@SpringBootApplication
//@EnableAutoConfiguration(exclude = {DataSourceAutoConfiguration.class})
public class PmsApplication {

	public static void main(String[] args) {
		SpringApplication.run(PmsApplication.class, args);
	}

}
