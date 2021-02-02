package dev.hyunlab.pms.service.impl;

import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.persistence.UserRepository;
import dev.hyunlab.pms.service.UserService;

@Service
public class UserServiceImpl implements UserService {

    @Autowired
    private UserRepository repo;

    @Override
    public User findByUserId(String userId) {
        Optional<User> opt = repo.findById(userId);
        if(opt.isPresent()){
            return repo.findByUserId(userId).get();
        }else{
            return null;
        }
    }

    @Override
    public List<User> findAll() {
        return repo.findAll();
    }

    @Override
    public User findByUserIdAndPasswd(String userId, String passwd) {
        Optional<User> opt = repo.findByUserIdAndPasswd(userId, passwd);
        if(opt.isPresent()){
            return opt.get();
        }
        
        return null;
    }
    
}
