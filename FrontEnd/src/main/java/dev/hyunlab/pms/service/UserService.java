package dev.hyunlab.pms.service;

import java.util.List;

import dev.hyunlab.pms.model.User;

public interface UserService {
    User findByUserId(String userId);

    List<User> findAll();

	User findByUserIdAndPasswd(String userId, String passwd);
}
