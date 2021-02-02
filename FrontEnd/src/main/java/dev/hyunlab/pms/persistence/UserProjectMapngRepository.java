package dev.hyunlab.pms.persistence;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import dev.hyunlab.pms.model.User;
import dev.hyunlab.pms.model.UserProjectMapng;

public interface UserProjectMapngRepository extends JpaRepository<UserProjectMapng, Integer>{

	/**
	 * 사용자 아이디로 목록 조회
	 * @param userId
	 * @return
	 */
	List<UserProjectMapng> findByUserUserId(String userId);
    
}
