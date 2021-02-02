package dev.hyunlab.pms.service.impl;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.model.UserProjectMapng;
import dev.hyunlab.pms.persistence.UserProjectMapngRepository;
import dev.hyunlab.pms.service.UserProjectMapngService;

@Service
public class UserProjectMapngServiceImpl implements UserProjectMapngService {
    @Autowired
    private UserProjectMapngRepository repo;

    @Override
    public List<UserProjectMapng> findByUserId(String userId) {
        return repo.findByUserUserId(userId);
    }
    
}
