package dev.hyunlab.pms.cmmn.interceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.AsyncHandlerInterceptor;

import dev.hyunlab.pms.cmmn.Tokens;
import lombok.extern.slf4j.Slf4j;

@Slf4j
public class SessionCheckInterceptor implements AsyncHandlerInterceptor {
    
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler){
        
        log.info("{} {}",request.getHeader("authorization"), request.getSession().getAttribute("user"));

        if("OPTIONS".equalsIgnoreCase(request.getMethod())){
            return true;
        }

        String auth = request.getHeader("authorization");
        if(null == auth){
            return false;
        }

        
        String token = auth.replaceAll("token ", "");
        if(null == Tokens.get(token)){
            return false;
        }

        //토큰정보 attribute에 설정
        request.setAttribute("token", token);

        return true;
    }
}
