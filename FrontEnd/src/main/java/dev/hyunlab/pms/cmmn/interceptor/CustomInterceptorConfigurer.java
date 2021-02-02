package dev.hyunlab.pms.cmmn.interceptor;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.InterceptorRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

/**
 * @see https://sunghs.tistory.com/84
 */
@Configuration
public class CustomInterceptorConfigurer implements WebMvcConfigurer {
    
    public void addInterceptors(InterceptorRegistry registry){
        registry.addInterceptor(sessionCheckInterceptor())
            .excludePathPatterns("/signin");
    }

    @Bean
    public SessionCheckInterceptor sessionCheckInterceptor(){
        return new SessionCheckInterceptor();
    }
}
