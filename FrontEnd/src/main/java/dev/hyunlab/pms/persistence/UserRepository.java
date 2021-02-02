package dev.hyunlab.pms.persistence;

import java.util.List;
import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;

import dev.hyunlab.pms.model.User;

public interface UserRepository extends JpaRepository<User, String> {
    Optional<User> findByUserId(String userId);

    List<User> findAll();

	Optional<User> findByUserIdAndPasswd(String userId, String passwd);
}
