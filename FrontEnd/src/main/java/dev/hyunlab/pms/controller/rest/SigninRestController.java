package dev.hyunlab.pms.controller.rest;

import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import dev.hyunlab.pms.cmmn.Tokens;
import dev.hyunlab.pms.model.Project;
import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.model.UserProjectMapng;
import dev.hyunlab.pms.service.UserService;
import lombok.extern.slf4j.Slf4j;

@CrossOrigin(origins = "*")
@RestController
@RequestMapping("/signin")
@Slf4j
/**
 * signin
 */
public class SigninRestController {

    @Autowired
    private UserService service;
    
    
    /**
     * sign in
     * @param request
     * @param user
     * @return
     */
    @PostMapping()
    public ResponseEntity<Map<String,Object>> signin(HttpServletRequest request, @RequestBody User user){
        Map<String,Object> map = new HashMap<>();
        map.put("resultMessage", "");


        //임시
        // user = service.findByUserIdAndPasswd(user.getUserId(), user.getPasswd());
        user = service.findByUserId(user.getUserId());
        if(null == user){
            map.put("resultMessage", "E_NOT_FOUND");
        }else{
            //토큰
            String token = request.getSession().getId();

            map.put("data", user);
            map.put("token", token);

            //저장. 세션대신 이거 사용. 클라이언트에서 항상 새로운 세션으로 접속하기 때문에
            Tokens.put(token, UserProjectMapng.builder().user(user).build());
            
        }

        log.debug("{}", map);
        return ResponseEntity.ok().body(map);
    }


    /**
     * 사용할 프로젝트 설정
     * @param request
     * @param project
     * @return
     */
    @PutMapping("/projects")
    public ResponseEntity<Map<String,Object>> setProject(HttpServletRequest request, @RequestBody Project project){
        String token = (String)request.getAttribute("token");
        Map<String,Object> map = new HashMap<>();

        //
        UserProjectMapng mapng = Tokens.get(token);
        if(null == mapng){
            map.put("resultMessage", "E_INVALID_TOKEN");
            return ResponseEntity.ok(map);
        }


        //
        mapng.setProject(project);
        Tokens.put(token, mapng);

        return ResponseEntity.ok(map);
    }
}
